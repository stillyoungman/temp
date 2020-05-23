import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import { isAuthenticated } from './services/authService';
import Auth from './Auth';


import './styles/main.css'

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

ReactDOM.render(isAuthenticated() ? (<BrowserRouter basename={baseUrl}><App /></BrowserRouter>) : <Auth basename={baseUrl} />, rootElement);

registerServiceWorker();