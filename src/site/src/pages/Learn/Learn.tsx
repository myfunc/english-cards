import React from "react";
import { CategoryCard, Title, Wrapper } from "../../components";

export function Learn() {  
    const arr = [
        { id: "1", name: "spark", learnedWords: 8, totalWords: 10, previewImageUrl: "https://image.flaticon.com/icons/svg/773/773058.svg" },
        { id: "2", name: "watch", learnedWords: 12, totalWords: 20, previewImageUrl: "https://image.flaticon.com/icons/svg/2922/2922319.svg" },
        { id: "3", name: "planet", learnedWords: 23, totalWords: 23, previewImageUrl: "https://image.flaticon.com/icons/svg/2772/2772505.svg" },
        { id: "4", name: "cosmonaut", learnedWords: 1, totalWords: 19, previewImageUrl: "https://image.flaticon.com/icons/svg/2772/2772557.svg" }
    ];
    return (
        <div className="learn">
            <Wrapper>
                <Title title="You can chose to learn" />
                <div className="learn__content">    
                    {arr.map(item => <CategoryCard key={item.id} {...item}/>)}
                </div>
            </Wrapper>
        </div>
    )
}