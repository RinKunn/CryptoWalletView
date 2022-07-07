import React from 'react'
import './Board.scss'

const Board = (props) => {
    return (
        <div className='board'>
            {props.children}
        </div>
    );
}

export default Board;