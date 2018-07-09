import { Transporte } from './transporte'
import { Estadia } from './estadia'
import { Atraccion } from './atraccion'

export class Paquete {
    id: number;
    nombre: string;
    fechaVencimiento: string;
    disponible: boolean;
    transporte: Transporte;
    estadia: Estadia;
    atraccion: Atraccion;
    costo: number;
}