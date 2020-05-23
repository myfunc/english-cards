import React, { useState, useEffect } from "react";
import { Card, Slide, Title, Wrapper } from "../../components";
import { Link } from "react-router-dom";

export function Word(props: any) {
    const arr = [
        { id: "1", nativeTranslation: "искра", foreignTranslations: ["spark"], imageUrl: "https://image.flaticon.com/icons/svg/773/773058.svg" },
        { id: "2", nativeTranslation: "часы", foreignTranslations: ["watch"], imageUrl: "https://image.flaticon.com/icons/svg/2922/2922319.svg" },
        { id: "3", nativeTranslation: "планета", foreignTranslations: ["planet"], imageUrl: "https://image.flaticon.com/icons/svg/2772/2772505.svg" },
        { id: "4", nativeTranslation: "космонавт", foreignTranslations: ["cosmonaut"], imageUrl: "https://image.flaticon.com/icons/svg/2772/2772557.svg" }
    ];

    const [active, isActive] = useState(0);
    const [direction, isDirection] = useState("");
    
    const slides = new Array(arr.length).fill("").map(_ => ({active : false, direction: "" }));

    slides[active].active = true;
    slides[active].direction = `-show-to-${direction}`;

    const prevHandler = () => {
        active === 0 ? isActive(arr.length - 1) : isActive(active - 1);
        isDirection("left");
    }

    const nextHandler = () => {
        active < arr.length - 1 ? isActive(active + 1) : isActive(0);
        isDirection("right");
    }

    useEffect(() => {
        slides[active].active = true;
        slides[active].direction = `-show-to-${direction}`;
    }, [active, direction, slides]);

    return (
        <div className="word">
            <Wrapper>
                <p className="word__return"><Link to="/learn" className="word__return-link">Return to all words</Link></p>
                <Title classes={["word__title"]} title={`You have chosed category "${props.match.params.id}"`} />
              
                <div className="word__content slider">
                    <button className="slider__control slider__control_prev" onClick={prevHandler}  />
                    <button className="slider__control slider__control_next" onClick={nextHandler} />
                    <div className="slider__content">
                        {arr.map((item, index) => 
                            <Slide key={index} active={slides[index].active} direction={slides[index].direction}><Card key={item.id} {...item} /></Slide>
                        )} 
                    </div>
                </div>
            </Wrapper>
        </div>
    )
}