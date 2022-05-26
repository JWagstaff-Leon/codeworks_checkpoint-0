export const dev = window.location.origin.includes('localhost')
export const baseURL = dev ? 'https://localhost:5001' : ''
export const useSockets = false
export const domain = 'dev-wlf420v1.us.auth0.com'
export const audience = 'about:blank'
export const clientId = 'D5lRqJ0bDJVlyQVzDR6XGqcpQltF6jQ5'