import React from 'react'
import styles from '../styles/list.module.css'
import { areEqual } from '../helpers'

const clientUserType = "client";
const masterUserType = "master";

export default class UserListItem extends React.Component {

    constructor(props){
        super(props)
        this.state = {
            ...props,
            isModified: false
        }
        this.setOriginalState(this.state);
    }

    setOriginalState({isBlocked, userType}){
        this.originalState = {isBlocked, userType};
    }

    shouldUpdateIsModifed = false;

    componentDidMount(){
        //call api
    }

    componentDidUpdate() {
        if (this.shouldUpdateIsModifed) {
            this.shouldUpdateIsModifed = false;
            let { isBlocked, userType } = this.state;
            this.setState({
                ...this.setState,
                isModified: !areEqual({ isBlocked, userType }, this.originalState)
            })
        }
    }

    toggleBlock = () => {
        this.shouldUpdateIsModifed = true;
        this.setState({
            ...this.state,
            isBlocked: !this.state.isBlocked
        })
    }

    save = () => {
        //call api
    }

    changeUserType = (e) =>{
        this.shouldUpdateIsModifed = true;
        this.setState({
            ...this.setState,
            userType: e.target.value
        })
    }

    render() {
        return <tr className={styles['list-item']}>
            <td>{this.props.children}</td>
            <td style={{ width: "10%", padding: "10px" }}>
                {
                    this.state.isBlocked
                        ? <button className="btn btn-sm btn-primary btn-block" onClick={this.toggleBlock}>Разблокировать</button>
                        : <button className="btn btn-sm btn-danger btn-block" onClick={this.toggleBlock}>Заблокировать</button>
                }
            </td>
            <td style={{ width: "10%", padding: "10px" }}>
                <select id="user-type" value={this.state.userType} onChange={this.changeUserType}>
                    <option value={clientUserType}>Клиент</option>
                    <option value={masterUserType} >Парикмахер</option>
                </select>
            </td>
            <td style={{ width: "10%", padding: "10px" }}>
                {this.state.isModified && <button className="btn btn-sm btn-success btn-block">Сохранить</button>}
            </td>
            <td style={{ width: "10%", padding: "10px" }}>
                {this.state.isModified && <button className="btn btn-sm btn-secondary btn-block"
                    onClick={() => {
                        this.shouldUpdateIsModifed = true;
                        this.setState({ ...this.state, ...this.originalState })
                    }}>Отменить</button>}
            </td>

        </tr>
    }
}