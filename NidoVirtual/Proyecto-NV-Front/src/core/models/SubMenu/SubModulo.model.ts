export class SubModuloModel {
    constructor(
        public idModulo: number = 0,
        public idSubModulo: number = 0,
        public subModuloDesc: string = '',
        public phat: string = '',
        public isVisible: boolean = false,
        public orden: number = 0,
        public modulo: string = '',
        public idSistema: number = 0,
        public sistema: string = '',
        public isSelecte: boolean = false,
        public idPermiso: number = 1
    ) {}
}
