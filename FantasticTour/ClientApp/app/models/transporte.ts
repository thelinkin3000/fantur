import { Ciudad } from './ciudad';

export class Transporte {
    id: number;
    fechaIda: string;
    fechaVuelta: string;
    costo: number;
    origenId: number;
    destinoId: number;
    tipoTransporte: string;
    tipoTransporteId: number;
}