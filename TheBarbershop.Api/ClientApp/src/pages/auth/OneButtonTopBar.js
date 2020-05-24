import React from 'react';
import { Link } from "react-router-dom";
import styles from '../../styles/auth.module.css'

export default ({ to, buttonText }) => <div className={styles["top-bar"]}>
    <Link to={to}>
        <button className={`btn btn-lg btn-primary btn-block ${styles["signup-btn"]}`}>{buttonText}</button>
    </Link>
</div>