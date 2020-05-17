import React from "react";
import { Title, Wrapper } from "./../../components"

export function Progress() {
    return (
        <div className="progress">
            <Wrapper classes={["progress__wrapper"]}>
                <Title title="Progress Page" />
            </Wrapper>     
        </div>
    )
}