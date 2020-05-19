import React, { useState } from "react";
import ReactDOM from 'react-dom'

export type ModalProps = {
    children: any
    beforeCloseCallback?: any
    classes?: string[]
}

export function Modal(props: ModalProps) {
    const [hideModal, isHideModal] = useState(true);

    
    const hideModalHandler = () => {
        isHideModal(false);
        props.beforeCloseCallback(false);
    }

    const classModifier = props.classes ? props.classes : []; 
    const classes = ["modal", ...classModifier];

    return ReactDOM.createPortal((
        hideModal ? (
            <div className={classes.join(" ")}>
                <div className="modal__overlay" onClick={hideModalHandler}></div>
                <div className="modal__content">
                    {props.children}
                </div>
            </div>
        ) : null), document.body
    )
}