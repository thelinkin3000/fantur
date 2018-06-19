import { Ciudad } from './ciudad';

export class Transporte {
    id: number;
    fecha: string;
    costo: number;
    origen: Ciudad;
    destino: Ciudad;
    tipo: String;
}