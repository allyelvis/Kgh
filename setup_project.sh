#!/bin/bash

# Exit immediately if a command exits with a non-zero status
set -e

# Prompt for GitHub repository URL
read -p "Enter your GitHub repository URL (e.g., https://github.com/yourusername/kgh-repertory.git): " repo_url

# Create project directory
mkdir kgh-repertory
cd kgh-repertory

# Initialize Git repository
git init

# Create directory structure
mkdir -p client/src/assets/icons client/src/assets/logo server/config server/models server/routes server/controllers server/middlewares server/utils server/plugins public

# Initialize React app in the client directory
npx create-react-app client

# Initialize Node.js project in the server directory
cd server
npm init -y
npm install express mongoose dotenv

# Create server files
cat > server.js <<EOL
const express = require('express');
const dotenv = require('dotenv');
const connectDB = require('./config/db');
const orders = require('./routes/orders');
const outlets = require('./routes/outlets');
const printers = require('./routes/printers');

dotenv.config();
connectDB();

const app = express();

app.use(express.json());
app.use('/api', orders);
app.use('/api', outlets);
app.use('/api', printers);

const PORT = process.env.PORT || 5000;

app.listen(PORT, () => {
    console.log(\`Server running on port \${PORT}\`);
});
EOL

cat > config/db.js <<EOL
const mongoose = require('mongoose');

const connectDB = async () => {
    try {
        await mongoose.connect(process.env.MONGO_URI, {
            useNewUrlParser: true,
            useUnifiedTopology: true,
        });
        console.log('MongoDB connected');
    } catch (error) {
        console.error('Error connecting to MongoDB:', error.message);
        process.exit(1);
    }
};

module.exports = connectDB;
EOL

cat > models/Order.js <<EOL
const mongoose = require('mongoose');

const orderSchema = new mongoose.Schema({
    item: { type: String, required: true },
    status: { type: String, required: true },
}, { timestamps: true });

const Order = mongoose.model('Order', orderSchema);
module.exports = Order;
EOL

cat > models/Outlet.js <<EOL
const mongoose = require('mongoose');

const outletSchema = new mongoose.Schema({
    name: { type: String, required: true },
    address: { type: String, required: true },
    contact: { type: String, required: true },
}, { timestamps: true });

const Outlet = mongoose.model('Outlet', outletSchema);
module.exports = Outlet;
EOL

cat > models/Printer.js <<EOL
const mongoose = require('mongoose');

const printerSchema = new mongoose.Schema({
    name: { type: String, required: true },
    type: { type: String, required: true },
    outlet: { type: mongoose.Schema.Types.ObjectId, ref: 'Outlet', required: true },
}, { timestamps: true });

const Printer = mongoose.model('Printer', printerSchema);
module.exports = Printer;
EOL

cat > routes/orders.js <<EOL
const express = require('express');
const { getOrders } = require('../controllers/orderController');
const router = express.Router();

router.get('/orders', getOrders);

module.exports = router;
EOL

cat > routes/outlets.js <<EOL
const express = require('express');
const { getOutlets, addOutlet } = require('../controllers/outletController');
const router = express.Router();

router.get('/outlets', getOutlets);
router.post('/outlets', addOutlet);

module.exports = router;
EOL

cat > routes/printers.js <<EOL
const express = require('express');
const { getPrinters, addPrinter } = require('../controllers/printerController');
const router = express.Router();

router.get('/printers', getPrinters);
router.post('/printers', addPrinter);

module.exports = router;
EOL

cat > controllers/orderController.js <<EOL
const Order = require('../models/Order');

const getOrders = async (req, res) => {
    try {
        const orders = await Order.find();
        res.status(200).json(orders);
    } catch (error) {
        res.status(500).json({ message: 'Error fetching orders' });
    }
};

module.exports = { getOrders };
EOL

cat > controllers/outletController.js <<EOL
const Outlet = require('../models/Outlet');

const getOutlets = async (req, res) => {
    try {
        const outlets = await Outlet.find();
        res.status(200).json(outlets);
    } catch (error) {
        res.status(500).json({ message: 'Error fetching outlets' });
    }
};

const addOutlet = async (req, res) => {
    try {
        const newOutlet = new Outlet(req.body);
        const savedOutlet = await newOutlet.save();
        res.status(201).json(savedOutlet);
    } catch (error) {
        res.status(500).json({ message: 'Error adding outlet' });
    }
};

module.exports = { getOutlets, addOutlet };
EOL

cat > controllers/printerController.js <<EOL
const Printer = require('../models/Printer');

const getPrinters = async (req, res) => {
    try {
        const printers = await Printer.find().populate('outlet');
        res.status(200).json(printers);
    } catch (error) {
        res.status(500).json({ message: 'Error fetching printers' });
    }
};

const addPrinter = async (req, res) => {
    try {
        const newPrinter = new Printer(req.body);
        const savedPrinter = await newPrinter.save();
        res.status(201).json(savedPrinter);
    } catch (error) {
        res.status(500).json({ message: 'Error adding printer' });
    }
};

module.exports = { getPrinters, addPrinter };
EOL

cat > plugins/samplePlugin.js <<EOL
// Sample plugin file
console.log("Sample plugin loaded");
EOL

# Create .env file for environment variables
cat > .env <<EOL
MONGO_URI=mongodb://localhost:27017/kgh-repertory
PORT=5000
EOL

# Go back to root directory
cd ..

# Create React components and services
cat > client/src/components/Header.js <<EOL
import React from 'react';
import logo from '../assets/logo/logo.png'; // Adjust the path as necessary
import './Header.css';

const Header = () => {
    return (
        <header>
            <img src={logo} alt="Restaurant Logo" className="logo" />
            <h1>Restaurant Management System</h1>
        </header>
    );
};

export default Header;
EOL

cat > client/src/components/Sidebar.js <<EOL
import React from 'react';
import dashboardIcon from '../assets/icons/dashboard.svg';
import ordersIcon from '../assets/icons/orders.svg';
import settingsIcon from '../assets/icons/settings.svg';
import './Sidebar.css';

const Sidebar = () => {
    return (
        <nav>
            <ul>
                <li>
                    <img src={dashboardIcon} alt="Dashboard" />
                    <span>Dashboard</span>
                </li>
                <li>
                    <img src={ordersIcon} alt="Orders" />
                    <span>Orders</span>
                </li>
                <li>
                    <img src={settingsIcon} alt="Settings" />
                    <span>Settings</span>
                </li>
            </ul>
        </nav>
    );
};

export default Sidebar;
EOL

cat > client/src/components/Panels/AdminPanel.js <<EOL
import React from 'react';

const AdminPanel = () => {
    return (
        <div>
            <h1>Admin Panel</h1>
            <p>Manage outlets, printers, and other settings.</p>
        </div>
    );
};

export default AdminPanel;
EOL

cat > client/src/components/Panels/KitchenPanel.js <<EOL
import React from 'react';

const KitchenPanel = () => {
    return (
        <div>
            <h1>Kitchen Panel</h1>
            <p>View and manage kitchen orders.</p>
        </div>
    );
};

export default KitchenPanel;
EOL

cat > client/src/components/Panels/OutletPanel.js <<EOL
import React from 'react';

const OutletPanel = () => {
    return (
        <div>
            <h1>Outlet Panel</h1>
            <p>Manage orders and customers for a specific outlet.</p>
        </div>
    );
};

export default OutletPanel;
EOL

cat > client/src/components/KOTPrinters/KOTPrinter.js <<EOL
import React, { useState, useEffect } from 'react';
import { fetchPrinters } from '../../services/api';

const KOTPrinter = () => {
    const [printers, setPrinters] = useState([]);

    useEffect(() => {
        const getPrinters = async ()
