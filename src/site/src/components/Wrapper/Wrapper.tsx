import React from "react";

export type WrapperPropp = {
    classes?: string[]
    children?: any
}

export function Wrapper(props: WrapperPropp) {
    const classModifier = props.classes ? props.classes : []; 
    const classes = ["wrapper", ...classModifier];

    return (
        <div className={classes.join(" ")}>
            {props.children}
        </div>
    )
}