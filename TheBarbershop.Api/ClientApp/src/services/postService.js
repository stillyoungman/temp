const posts = {
    1: {
        id: 1,
        title: "Заголовок",
        text: "ng Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in th"
    }
}

let index = 2;


export default {
    getPosts: () => {
        return new Promise((resolve, reject) => {
            resolve(posts)
        })
    },
    updatePost: (post) => {
        return new Promise((resolve, reject) => {
            posts[post.id] = post;
            resolve();
        })
    },
    createPost(post) {
        post.id = index++;
        return this.updatePost(post);
    }
}