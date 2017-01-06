export class ClientAuthenticationToken{

    // Access token which is used for api calling.
    public token: string;

    // Type of token.
    public type: string;

    // When the token should be expired.
    public expire: number;
}