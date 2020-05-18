import React, { useState, useEffect } from "react";

export type SlideProps = {
    active: boolean
    direction: string
    children: any
}

export function Slide(props: SlideProps) {
    const [active, isActive] = useState(false);
    const [direction, isDirection] = useState("");

    useEffect(() => {
        isActive(props.active);
        isDirection(props.direction);
    }, [props]);

    return (
        <div className={`slide${active ? " -active" : ""}${direction ? " " + direction : ""}`}>
           {props.children}
        </div>
    )
}