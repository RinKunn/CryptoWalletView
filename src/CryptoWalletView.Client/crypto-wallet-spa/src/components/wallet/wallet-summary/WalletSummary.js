import React from 'react'
import './WalletSummary.scss'

const WalletSummary = ({ total, changeVal, changePerc, number, numberBlocked}) => {
    return (
        <>
            <h2 className='wsummary__total'>{toCurrency(total)}</h2>
            <p className={`wsummary__change ${changeVal > 0 ? 'wsummary--profit' : 'wsummary--loss'}`}>
                <span>{`${changeVal> 0 ? '+' : ''} ${toCurrency(changeVal)}`}</span>
                <span>{`${changePerc > 0 ? '+' : ''} ${toPercent(changePerc)}`}</span>
                <span className='wsummary__changeInfo'>1D</span>
            </p>
            <p className='wsummary__desc'>Currenies: {number} / Blocked: { numberBlocked}</p>
        </>
    );
}

const toCurrency = (value) => value.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
const toPercent = (value) => (100 * value).toFixed(2) + '%' //toLocaleString('en-US', { style: 'percent'});

export default WalletSummary;