import React, { useState } from "react";
import { Login, Modal, Wrapper } from "./../../components";
import logo from './../../svg/unicorn.svg';
import { NavLink } from "react-router-dom";

export function Header() {
    const [showModal, isShowModal] = useState(false);

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

    const loginBtnHandler = () => {
        isShowModal(true);
    }

    return (
        <header className="header">
            <Wrapper classes={["header__wrapper"]}>
                <img src={logo} className="header__logo" alt="unicorn"/> 
                <p className="header__title">Learn English Words</p>
                <nav>
                    {headerLinks.map(item => <NavLink key={item.label} className="header__link" to={item.to} exact={item.exact}>{item.label}</NavLink>)}
                    <button type="button" className="header__btn" onClick={loginBtnHandler}>Login</button>
                </nav>
            </Wrapper>
            {showModal && 
                <Modal classes={["login"]} beforeCloseCallback={isShowModal}>
                    <Login />
                </Modal>
            }
        </header>
    )
}