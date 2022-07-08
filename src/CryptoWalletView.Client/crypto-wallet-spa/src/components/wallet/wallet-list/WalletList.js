import React, { useEffect, useState } from 'react'
import './WalletList.scss'
import WalletItem from './wallet-item/WalletItem';

const WalletList = ({ assets }) => {
    return (
        <ul className='walletList__list'>
            {assets.map(asset => <li><WalletItem data={asset} /></li>)}
        </ul>
    );
}


export default WalletList;