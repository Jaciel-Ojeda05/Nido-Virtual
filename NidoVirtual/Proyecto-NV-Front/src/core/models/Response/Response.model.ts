export class ResponseModel<T> {
    constructor(
        public codigo: number = 0,
        public mensaje: string = '',
        public data: T | null = null
    ) {}
}
