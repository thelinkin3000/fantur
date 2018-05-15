import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';


import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { HotelesComponent } from './components/hoteles/hoteles.component';
import { HotelEditorComponent } from './components/hotel-editor/hotel-editor.component';
import { AtraccionesComponent } from './components/atracciones/atracciones.component';
import { AtraccionEditorComponent } from './components/atraccion-editor/atraccion-editor.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        HotelesComponent,
        HotelEditorComponent,
        AtraccionesComponent,
        AtraccionEditorComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        HttpClientModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'hoteles', component: HotelesComponent },
            { path: 'hotel-editor', component: HotelEditorComponent },
            { path: 'atraccion-editor', component: AtraccionEditorComponent },
            { path: 'atracciones', component: AtraccionesComponent},
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
