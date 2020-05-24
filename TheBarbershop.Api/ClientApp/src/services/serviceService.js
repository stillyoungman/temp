const services = { 
    1: {
        id: 1,
        type: 1,
        name: "Теннис",
        price: 300
    },
    2: {
        id: 2,
        type: 1,
        name: "Undercut",
        price: 200
    }
}


export default {
    getServies() {
        return new Promise((resolve, reject) => {
            resolve(Object.values(services))
        });
    }    
}