import React, { useState } from "react";

export function Login() {

    const [valid, isValid] = useState(false);

    const authData = {
        login: "testUser",
        password: "Aa123456"
    }

    const options = {
        method: 'POST',
        body: JSON.stringify(authData)
    }

    type formControlItem = {
        classes: string
        valid: boolean
        handler: any
        autoComplete: string
        placeholder: string
        type: string
        datatype: "login" | "password"
    }

    type formControls = {
        login: formControlItem,
        password: formControlItem
    };

    const changeHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
        let isFormValid = false;
        let item: keyof formControls;

        let type  = e.target.dataset.type;

        if (type === "login" || type === "password") {
            if (e.target.value.trim().length) {
                formControls[type].valid = true;
            } else {
                formControls[type].valid = false;
            }
        }

        for (item in formControls) {
            isFormValid = formControls[item].valid;
        }

        isValid(isFormValid);
    }

    const formControls: formControls = {
        login: {
            classes: "login__input login__login",
            valid: false,
            handler: changeHandler,
            autoComplete: "user-name",
            placeholder: "Login",
            type: "text",
            datatype: "login"
        },
        password: {
            classes: "login__input login__password",
            valid: false,
            handler: changeHandler,
            autoComplete: "current-password",
            placeholder: "Password",
            type: "password",
            datatype: "password"
        }
    };

    const loginHandler = async () => {
        try {
            let response = await fetch("http://localhost:9999/login", options);
            console.log(response);
        } catch(error) {
            // throw new Error(error);
            console.error(error);
        }
    }

    return (
        <form className="login__wrapper">
            <h2 className="login__title">Nice to see you ^^</h2>
            <p className="login__subtitle">Please, login to get access</p>
            {
                // Object.keys(formControls).map((controlName, index) => {
                //     const control = formControls[controlName]
                //     return (
                //       <input
                //         key={controlName + index}
                //         type={control.type}
                //         datatype={control.datatype}
                //         placeholder={control.placeholder}
                //         autoComplete={control.autoComplete}
                //         onChange={control.handler}
                //       />
                //     )
                //   })
                
            }
            <input className="login__input login__name" data-type="login" onChange={changeHandler} autoComplete="user-name" placeholder="Login" type="text"/>
            <input className="login__input login__password" data-type="password" onChange={changeHandler} autoComplete="current-password" placeholder="Password" type="password"/>
            <button className="login__button" type="button" disabled={!valid} onClick={loginHandler}>Login</button>
        </form>
    )
}