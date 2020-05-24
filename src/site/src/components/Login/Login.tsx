import React, { useState } from "react";

export type LoginProps = {
    beforeCloseCallback?: (param: any) => void
}

export function Login(props: LoginProps) {

    type formControlItem = {
        classes: string
        valid: boolean
        isError: boolean
        minLength: number
        errorMsg: () => string
        autoComplete: string
        placeholder: string
        type: string
        datatype: "login" | "password",
        value: string
    }

    type formControls = {
        [name : string]: formControlItem,
    };

    const commonFormControlItem = {
        valid: false,
        isError: false,
        value: ""
    }

    const formControlsInitial: formControls = {
        login: {
            ...commonFormControlItem,
            classes: "login__input login__login",
            minLength: 4,
            errorMsg() {
                return (() => `Min ${this.minLength} symbols`)()
            },
            autoComplete: "user-name",
            placeholder: "Login",
            type: "text",
            datatype: "login",
        },
        password: {
            ...commonFormControlItem,
            classes: "login__input login__password",
            minLength: 6,
            errorMsg() {
                return (() => `Min ${this.minLength} symbols`)()
            },
            autoComplete: "current-password",
            placeholder: "Password",
            type: "password",
            datatype: "password",
        }
    }; 

    const [valid, isValid] = useState(false);
    const [formControls, setFormControls] = useState(formControlsInitial);

    const keysFormControls = Object.keys(formControls);

    const changeFormControls = <K extends keyof formControls, T extends keyof formControlItem>(type: K, key: T, value: formControlItem[T]) => {
        const newItem = {...formControls[type]};
        newItem[key] = value;
        setFormControls({...formControls, [type]: newItem });
    }

    const changeHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
        let isFormValid = false;
        let item: keyof formControls;

        let type = e.target.dataset.type;

        if (type && keysFormControls.includes(type)) {
            if (e.target.value.trim().length >= formControls[type].minLength) {
                changeFormControls(type, "valid", true);
            } else {
                changeFormControls(type, "valid", false);
            }
        }

        for (item in formControls) {
            isFormValid = formControls[item].valid;
        }

        isValid(isFormValid);
    }
    

    const blurHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
        let type = e.target.dataset.type;

        if (type && keysFormControls.includes(type)) {
            if (e.target.value.trim().length >= formControls[type].minLength) {
                const newItem = {...formControls[type]};
                newItem.value = e.target.value;
                newItem.isError = false;
                setFormControls({...formControls, [type]: newItem });
            } else {
                const newItem = {...formControls[type]};
                newItem.value = e.target.value;
                newItem.isError = true;
                setFormControls({...formControls, [type]: newItem });
            }
        }
    }

    const focusHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
        let type = e.target.dataset.type;

        if (type && keysFormControls.includes(type)) {
            changeFormControls(type, "isError", false);
        }
    }

    const loginHandler = async () => {
        // const authData = {
        //     login: "testUser",
        //     password: "Aa123456"
        // }

        const authData = {
            login: formControls.login.value,
            password: formControls.password.value
        }
        
        const options = {
            method: "POST",
            body: JSON.stringify(authData)
        }

        try {
            let response = await fetch("http://localhost:9999/api/login", {...options, credentials: "include"});
            console.log(response);
            if (props.beforeCloseCallback) {
                props.beforeCloseCallback(false);
            }
        } catch(error) {
            // throw new Error(error);
            console.error(error);
        }        
    }

    return (
        <form className="login__wrapper">
            <h2 className="login__title">Nice to see you ^^</h2>
            <p className="login__subtitle">Please, login to get access</p>
            {keysFormControls.map(key => 
                <label className="login__field" key={key}>
                    <input 
                        data-type={formControls[key].datatype}
                        type={formControls[key].type}
                        placeholder={formControls[key].placeholder}
                        autoComplete={formControls[key].autoComplete}
                        onChange={changeHandler}
                        onBlur={blurHandler}
                        onFocus={focusHandler}
                        className={formControls[key].classes}
                    />
                    <span className={`login__error-msg ${formControls[key].isError ? "-show" : "-hide"}`}>{formControls[key].errorMsg()}</span>
                </label>
            )}
            <button className="login__button" type="button" disabled={!valid} onClick={loginHandler}>Login</button>
        </form>
    )
}