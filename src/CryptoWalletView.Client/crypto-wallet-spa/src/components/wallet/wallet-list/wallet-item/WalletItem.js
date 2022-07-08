import React from 'react'
import './WalletItem.scss';

const WalletItem = ({ data }) => {
    return (
        <div className={`walletItem ${!data.coinInfo.isTrading ? 'walletItem--blocked' : ''}`}>
            <img className='walletItem__img'
                alt={data.coinInfo.code}
                src={data.coinInfo.imageSource} //TODO: find new source
                // src={`${process.env.PUBLIC_URL}/icon/${data.coinInfo.code}.png`} 
            />
            <p className='walletItem__title'>
                {data.coinInfo.code}
                {' '}
                <span className='walletItem__name'>{data.coinInfo.name}</span>
            </p>
            <p className='walletItem__amount'>
                {data.amount}
                {' '}
                <span className='walletItem__price'>{toCurrency(data.price)}</span>
            </p>
            <p className='walletItem__total'>
                {toCurrency(data.amount * data.price)}
            </p>
            <p className={`walletItem__change ${(data.changeValue > 0 && 'walletItem__change--profit')} ${(data.changeValue < 0 && 'walletItem__change--loss')}`}>
                {`${data.changeValue> 0 ? '+' : ''} ${toCurrency(data.changeValue)}`}
                {' / '}
                {`${data.changePercent > 0 ? '+' : ''} ${toPercent(data.changePercent)}`}
            </p>
        </div>
    );
}

const toCurrency = (value) => value.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
const toPercent = (value) => (100 * value).toFixed(2) + '%' //toLocaleString('en-US', { style: 'percent'});
export default WalletItem;