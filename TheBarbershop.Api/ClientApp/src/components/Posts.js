import React, { Component } from 'react';
import { PostListItem } from './PostListItem';
import { Modal } from 'react-bootstrap'
import { withRouter } from 'react-router-dom'
import { postEditorPath } from "../constants"
import postService from "../services/postService"

class Posts extends Component {

  constructor(props) {
    super(props);

    this.state = {
      isEditingEnabled: false,
      showModal: false
    }

    this.history = props.history
  }

  componentDidMount = () => {
    postService.getPosts().then(p => {
      this.setState({
        ...this.state,
        posts: p
        //posts: p.reduce((result, post) => ({ ...result, [post.id]: post }), {}) 
      },
        () => console.log(this.state.posts))
    })
  }

  closeModal = () => this.setState({ ...this.setState, showModal: false });

  showModal = (modalTitle, modalText) => {
    this.setState({
      ...this.state,
      showModal: true,
      modalTitle,
      modalText
    })
  }

  deletePost = (title, id) => {
    const approved = window.confirm(`Вы дейтвительно хотите удалить пост '${title}'?`);
    if (approved) {
      //call api
      //filter source
    }
  }

  onSaveEditPost = (id, title, text) => {
    if (!id) {
      postService.createPost({ title, text })
        .then(() => {
          this.history.onSaveEditCallback = undefined;
          this.history.goBack();
        })
    } else {
      postService.updatePost({ id, title, text })
        .then(() => {
          this.history.onSaveEditCallback = undefined;
          this.history.goBack();
        })
    }

  }

  editPost = ({ id, title, text }, force = false) => {
    //write saveCallback to history object as workaround
    this.history.onSaveEditCallback = this.onSaveEditPost;
    this.history.push({ pathname: postEditorPath, state: { id, text, title, force } })
  }

  render() {
    return (
      <>
        <table style={{ width: '100%' }}>
          <tbody>
            {
              this.state.posts &&
              Object.values(this.state.posts).map(p => <PostListItem
                key={p.id}
                onSelect={() => this.showModal(p.title, p.text)}
                onDelete={() => this.deletePost(p.title, p.id)}
                onEdit={() => this.editPost(p)}
                title={p.title} />)
            }
          </tbody>
        </table >
        <br />
        <button onClick={() => this.editPost({}, true)} className="btn btn-success">Добавить</button>

        {
          this.state.showModal &&
          <>
            <Modal show={this.state.showModal} onHide={this.closeModal}>
              <Modal.Header closeButton>
                <Modal.Title>{this.state.modalTitle}</Modal.Title>
              </Modal.Header>
              <Modal.Body>{this.state.modalText}</Modal.Body>
            </Modal>
          </>
        }

      </>
    );
  }
}


export default withRouter(Posts);