import React from 'react'
import './WalletItem.scss'

const WalletItem = ({ data }) => {
    return (
        <div className='walletItem'>
            <img className='walletItem__img' src={data.symbol.imgSrc} alt={data.symbol.code} />
            <p className='walletItem__title'>
                {data.symbol.code}
                {' '}
                <span className='walletItem__name'>{data.symbol.name}</span>
            </p>
            <p className='walletItem__amount'>
                {data.amount}
                {' '}
                <span className='walletItem__price'>{toCurrency(data.price)}</span>
            </p>
            <p className='walletItem__total'>
                {toCurrency(data.total)}
            </p>
            <p className={`walletItem__change ${data.change.value > 0 ? 'walletItem__change--profit' : 'walletItem__change--loss'}`}>
                {`${data.change.value > 0 ? '+' : ''} ${toCurrency(data.change.value)}`}
                {' / '}
                {`${data.change.value > 0 ? '+' : ''} ${toPercent(data.change.value)}`}
            </p>
        </div>
    );
}

const toCurrency = (value) => value.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
const toPercent = (value) => value.toLocaleString('en-US', { style: 'percent'});
export default WalletItem;