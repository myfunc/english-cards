import React from "react";
import { Wrapper } from "./../../components";
import logo from './../../svg/unicorn.svg';
import { NavLink } from "react-router-dom";

export function Header() {
    const headerLinks = [{
        to: "/",
        label: "Home",
        exact: true
    }, {
        to: "/learn",
        label: "Learn"
    }, {
        to: "/progress",
        label: "Progress"
    }];

    return (
        <header className="header">
            <Wrapper classes={["header__wrapper"]}>
                <img src={logo} className="header__logo" alt="unicorn"/> 
                <p className="header__title">Learn English Words</p>
                <ul>
                    {headerLinks.map(item => <NavLink className="header__link" to={item.to} exact={item.exact}>{item.label}</NavLink>)}
                </ul>
            </Wrapper>
        </header>
    )
}