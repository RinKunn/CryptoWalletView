import './App.scss';
import Board from './components/board/Board';
import WalletCard from './components/card/wallet-card/WalletCard';
import Wallet from './components/wallet/Wallet';

function App() {
  const assets = [
    {
      symbol: {
        name: 'Ripple',
        code: 'XRP',
        imgSrc: 'https://cryptoicons.org/api/color/xrp/50',
      },
      amount: 162.3212,
      price: 0.543,
      total: 162.3212 * 0.543,
      change: {
        value: 162.3212 * 0.1,
        percent: 0.543 / (0.543 - 0.1) - 1,
      }
    },
    {
      symbol: {
        name: 'Bitcoin',
        code: 'BTC',
        imgSrc: 'https://cryptoicons.org/api/color/btc/50',
      },
      amount: 1.3212,
      price: 20000.92,
      total: (20000.92 * 1.3212),
      change: {
        value: -1.3212 * 100,
        percent: 1 - (20000.92 / (20000.92 + 100)),
      }
    }
  ]

  
  return (
    <div className="App">
      <Wallet />
    </div>
  );
}

export default App;
