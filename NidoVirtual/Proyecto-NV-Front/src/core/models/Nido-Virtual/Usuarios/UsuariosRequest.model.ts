export class UsuariosRequestModel{
    constructor(        
        public nombreUsuario: string = "",
        public apePaterno: string = "",
        public apeMaterno: string = "",
        public correo: string = "",
        public userPassword: string = "",
        public isAdmin: boolean = false
    ){}
}
