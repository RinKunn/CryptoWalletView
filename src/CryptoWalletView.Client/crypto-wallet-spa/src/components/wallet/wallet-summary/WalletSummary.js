import React from 'react'
import './WalletSummary.scss'

const WalletSummary = ({ total, changeVal, changePerc }) => {
    return (
        <>
            <h2 className='wsummary__total'>${total}</h2>
            <p className={`wsummary__change ${changeVal > 0 ? 'wsummary--profit' : 'wsummary--loss'}`}>
                <span>{(changeVal > 0 ? "+" : "-") + "$" + Math.abs(changeVal)}</span>
                <span>{(changePerc > 0 && "+") + changePerc}%</span>
            </p>
        </>
    );
}

export default WalletSummary;