import React from 'react'
import './WalletList.scss'
import WalletItem from './wallet-item/WalletItem';

const WalletList = ({ assets }) => {
    return (
        <>
            <h1 className='walletList__title'>Wallet</h1>
            <ul className='walletList__list'>
                {assets.map(asset => <li><WalletItem data={asset} /></li>)}
            </ul>
        </>
    );
}

export default WalletList;