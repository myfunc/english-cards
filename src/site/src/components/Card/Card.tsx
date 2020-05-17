import React, { useState } from "react";

export type CardProps = {
    id: string,
    nativeTranslation: string
    foreignTranslations: string[]
    imageUrl: string
    active: boolean
}

Card.defaultProps = {
    imageUrl: "https://image.flaticon.com/icons/svg/2772/2772508.svg"
}

export function Card(props: CardProps) {
    const [translated, isTranslated] = useState(false);

    const changeHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (props.foreignTranslations.includes(e.target.value)) {
            isTranslated(true);
        } else {
            isTranslated(false);
        }
    }

    return (
        <div id={props.id} className={`card ${translated ? " card_translated" : ""}`}>
            <img className="card__img" src={props.imageUrl} alt={props.imageUrl} />
            <p className="card__native-translation">{props.nativeTranslation}</p>
            <input className="card__input" type="text" placeholder="Translate" onChange={changeHandler} />
        </div>
    )
}