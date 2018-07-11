import { Transporte } from './transporte'
import { Estadia } from './estadia'
import { Atraccion } from './atraccion'

export class Paquete {
    id: number;
    nombre: string;
    fechaVencimiento: string;
    disponible: boolean;
    transporteId: number;
    estadiaId: number;
    atraccionId: number;
    atraccion: string;
    hotel: string;
    costo: number;
}