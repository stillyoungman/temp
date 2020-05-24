import React from 'react'
import { withRouter } from 'react-router-dom'
import styles from '../styles/posts.module.css'

class PostEditor extends React.Component {

    constructor(props) {
        super(props)
        this.state = {
            ...props.location.state,

        }

        this.history = props.history

        //go to main, if post object empty
        if ((!this.state.title || !this.state.text) && !this.state.force) {
            this.history.replace("/");
        }
    }

    componentWillUnmount(){
        this.history.onSaveEditCallback = undefined;
    }

    handleSave = () => {
        if(!this.state.title){
            alert("Пустой заголовок недопустим.")
        } else if(!this.state.text){
            alert("Пост должен содержать текст.")
        } else {
            this.history.onSaveEditCallback(this.state.id, this.state.title, this.state.text)
        }
    }

    render() {
        return <>
            <input
                style={{
                    margin: "10px 0px",
                    fontFamily: "Helvetica, Verdana, sans-serif",
                    fontSize: "x-large",
                    border: 'none',
                    fontWeight: 'bold'
                }}
                value={this.state.title}
                onChange={({ target }) => this.setState({ ...this.state, title: target.value })}
                placeholder="Заголовок" />
            <br />
            <textarea
                onChange={({ target }) => this.setState({ ...this.state, text: target.value })}
                value={this.state.text}
                placeholder="Текст..." />
            <br />
            <button style={{ margin: "5px 0px" }} onClick={this.handleSave} className="btn btn-success">Сохранить</button>
            <button style={{ margin: "5px 10px" }} onClick={() => this.history.goBack()} className="btn btn-secondary">Отменить</button>
        </>
    }
}

export default withRouter(PostEditor);
