import React from 'react'
import styles from '../styles/list.module.css'

export class PostListItem extends React.Component {


    render() {
        return <tr className={styles['list-item']}>
            <td onClick={() => this.props.onSelect && this.props.onSelect()} style={{ paddingLeft: '10px' }}>{this.props.title}</td>
            <td style={{ width: "10%", padding: "10px" }}>
                <button onClick={this.props.onEdit} className="btn btn-sm btn-primary btn-block">Редактировать</button>
            </td>
            <td style={{ width: "10%", padding: "10px" }}>
                <button onClick={this.props.onDelete} className="btn btn-sm btn-danger btn-block">Удалить</button>
            </td>

        </tr>
    }
}