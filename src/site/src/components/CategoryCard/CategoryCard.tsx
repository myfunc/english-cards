import React from "react";
import { NavLink } from "react-router-dom";

export type CategoryCardProps = {
    id: string,
    name: string
    totalWords: number,
    learnedWords: number,
    previewImageUrl: string
}

CategoryCard.defaultProps = {
    previewImageUrl: "https://image.flaticon.com/icons/svg/2772/2772508.svg"
}

export function CategoryCard(props: CategoryCardProps) {
    return (
        <div id={props.id} className="category-card">
            <p className="category-card__progress">{props.learnedWords}/{props.totalWords}</p>
            <img className="category-card__img" src={props.previewImageUrl} alt={props.previewImageUrl} />
            <NavLink to={`/learn/${props.name}`}><p className="category-card__name">{props.name}</p></NavLink>
        </div>
    )
}