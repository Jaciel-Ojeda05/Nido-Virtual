export class UsuariosResponseModel {
    constructor(
        public idCliente: number = 0,
        public nombreUsuario: string = "",
        public apePaterno: string = "",
        public apeMaterno: string = "",
        public correo: string = "",
        public userPassword: string = "",
        public isAdmin: boolean = false
    ) { }
}
