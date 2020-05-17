import React from "react";
import { Header } from "./../../components";

export function Layout(props: any) {
    return (
        <div className="layout">
            <Header />
            {props.children}
        </div>
    )
}