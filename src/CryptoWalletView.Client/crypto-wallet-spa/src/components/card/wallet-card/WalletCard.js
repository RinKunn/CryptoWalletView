import React from 'react'
import '../Card.scss'
import Card from '../Card'
import WalletSummary from '../../wallet/wallet-summary/WalletSummary';

const WalletCard = () => {
    return (
        <Card title="Wallet" linkText="View all" >
            <WalletSummary total={676.92} changeVal={10} changePerc={1.2} />
        </Card>
    );
}

export default WalletCard;