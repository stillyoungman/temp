import React from 'react'
import styles from '../styles/list.module.css'

class ServiceListItem extends React.Component {

    constructor(props) {
        super(props)
        this.state = {
            ...props
        }
    }

    render() {
        return <>
            <tr className={styles['list-item']}>
                <td>{this.state.name}</td>
                <td style={{ width: "10%", padding: "10px" }}>{this.state.price}</td>
                <td style={{ width: "10%", padding: "10px" }}><button className="btn btn-sm btn-danger btn-block">Удалить</button></td>
            </tr>
        </>
    }
}

export default ServiceListItem;