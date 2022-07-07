import React from 'react'
import './Card.scss'

const Card = (props) => {
    return (
        <div className='card'>
            <header className='card__header'>
                <p className='card__title'>{props.title}</p>
                <a className='card__link' href={props.link}>{ props.linkText}</a>
            </header>
            <div className='card__content'>
                {props.children}
            </div>
        </div>
    );
}

export default Card;