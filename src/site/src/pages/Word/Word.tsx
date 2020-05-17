import React from "react";
import { Card, Title, Wrapper } from "../../components";
import { NavLink } from "react-router-dom";

export function Word(props:any) {
    const arr = [
        { id: "1", nativeTranslation: "искра", foreignTranslations: ["spark"], imageUrl: "https://image.flaticon.com/icons/svg/773/773058.svg" },
        { id: "2", nativeTranslation: "часы", foreignTranslations: ["watch"], imageUrl: "https://image.flaticon.com/icons/svg/2922/2922319.svg" },
        { id: "3", nativeTranslation: "планета", foreignTranslations: ["planet"], imageUrl: "https://image.flaticon.com/icons/svg/2772/2772505.svg" },
        { id: "4", nativeTranslation: "космонавт", foreignTranslations: ["cosmonaut"], imageUrl: "https://image.flaticon.com/icons/svg/2772/2772557.svg" }
    ];
    let currentWords = arr.filter(item => !item.foreignTranslations.indexOf(props.match.params.id));
    return (
        <div className="word">
            <Wrapper>
                <p className="word__return"><NavLink to="/learn" className="word__return-link">Return to all words</NavLink></p>
                <Title classes={["word__title"]} title={`You have chosed category "${currentWords[0].foreignTranslations[0]}"`} />
                <div className="word__content">    
                    <Card {...currentWords[0]}/>
                </div>
            </Wrapper>
        </div>
    )
}