import { SubModuloModel } from "../SubMenu/SubModulo.model";

export class ModuloModel {
    constructor(
        public idModulo: number = 0,
        public moduloDesc: string = '',
        public icono: string = '',
        public idSistema: number = 0,
        public orden: number = 0,
        public isVisible: boolean = false,
        public sistema: string = '',
        public isSelecte: boolean = false,
        public submodules: SubModuloModel[] = new Array<SubModuloModel>(),
        public phat: string = ''
    ) {}
}
