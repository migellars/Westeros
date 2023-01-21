export interface TokenModel {
    data: {
        token: string;
        expiresIn: string;
        username: string
    }
    access_token: string;
    refresh_token: string;
}
