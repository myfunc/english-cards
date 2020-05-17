import React, { useState, useEffect } from "react";

export type SlideProps = {
    active: boolean
    children: any
}

export function Slide(props: SlideProps) {
    const [active, isActive] = useState(false);

    useEffect(() => {
        isActive(props.active);
    }, [props.active]);

    return (
        <div className={`slide${active ? " -active" : ""}`}>
           {props.children}
        </div>
    )
}