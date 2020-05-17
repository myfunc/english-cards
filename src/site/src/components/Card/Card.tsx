import React from "react";

export type CardProps = {
    id: string,
    nativeTranslation: string
    foreignTranslations?: string[]
    imageUrl: string
}

Card.defaultProps = {
    imageUrl: "https://image.flaticon.com/icons/svg/2772/2772508.svg"
}

export function Card(props: CardProps) {
    return (
        <div id={props.id} className="card">
            <img className="card__img" src={props.imageUrl} alt={props.imageUrl} />
            <p className="card__native-translation">{props.nativeTranslation}</p>
            <input className="card__input" type="text" placeholder="Translate" />
        </div>
    )
}