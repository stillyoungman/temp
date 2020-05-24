import React from 'react';
import ServiceListItem from './ServiceListItem';
import serviceService from '../services/serviceService'

class Services extends React.Component {

    constructor(props) {
        super(props)
        this.state = {
            isEditing: false
        }
    }

    componentDidMount() {
        serviceService.getServies().then((services) => {
            this.setState({ ...this.state, services })
        })
    }

    toggleIsEditing = () => {
        this.setState({ ...this.state, isEditing: !this.state.isEditing })
    }

    createNewService(){

    }

    removeService(){

    }

    updateServie(){
        
    }

    render() {
        return <>
            <table style={{ width: '100%' }}>
                <tbody>
                    {
                        this.state.services &&
                        this.state.services.map((s) => <ServiceListItem key={s.id}
                            name={s.name}
                            price={s.price}
                        />)
                    }
                    {
                        this.state.isEditing &&
                        <tr>
                            <td>
                                <input style={{ margin: 0, width:"auto" }} />
                            </td>
                            <td style={{ width: "10%", padding: "10px" }}>
                                <input style={{ margin: 0, width:"auto" }} type="number" inputMode="decimal" />
                            </td>
                            <td style={{ width: "10%", padding: "10px" }}>
                                <button onClick={this.toggleIsEditing} className="btn btn-sm btn-danger btn-block">Отменить</button>
                            </td>
                        </tr>
                    }
                </tbody>
            </table>
            <br />
            <button onClick={() => {
                if(!this.state.isEditing) { this.toggleIsEditing() }
            }} className="btn btn-success">{this.state.isEditing ? "Сохранить" : "Добавить"}</button>
        </>
    }
}

export default Services;