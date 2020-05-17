import React from "react";

export type TitlePropp = {
    classes?: string[]
    children?: any
    title?: string
}

export function Title(props: TitlePropp) {
    const classModifier = props.classes ? props.classes : []; 
    const classes = ["title", ...classModifier];

    return (
        <h1 className={classes.join(" ")}>
            {props.title}
            {props.children}
        </h1>
    )
}