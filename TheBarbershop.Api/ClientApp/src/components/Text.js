import React from 'react'

export default ({ children, isBold }) => {
    if (isBold) {
        return <b>{children}</b>
    } else {
        return <p>{children}</p>
    }
}