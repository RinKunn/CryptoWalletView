import React, { useState, useEffect } from 'react'
import WalletList from './wallet-list/WalletList';
import WalletSummary from './wallet-summary/WalletSummary';
import './Wallet.scss';

const Wallet = () => {
    const [isLoading, setIsLoading] = useState(true);
    const [wallet, setWallet] = useState(null);

    useEffect(() => {
        if (!isLoading) return;
        fetch('https://localhost:7266/Wallet')
            .then(response => response.json())
            .then((response) => {
                setWallet(response);
                setIsLoading(prev => false);
            })
    }, []);

    
    return (
        <>
            <header className='wallet__header'>
                <div className='container'>
                    <h1 className='wallet__title'>Crypto Wallet</h1> 
                </div>
            </header>
            <div className='container'>
                {isLoading === true
                    ? <h2 className='loading'>Loading...</h2>
                    :
                    <>
                        <WalletSummary total={wallet.total} changeVal={wallet.changeValue} changePerc={wallet.changePercent}
                            numberBlocked = {wallet.assets.filter(a => a.coinInfo.isTrading === false).length}
                            number={ wallet.assets.length - wallet.assets.filter(a => a.coinInfo.isTrading === false).length} />
                        <WalletList assets={wallet.assets} />
                    </>
                    }
            </div>
        </>
    );
}

export default Wallet;