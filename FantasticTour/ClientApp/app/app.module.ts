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
import { PaquetesComponent } from './components/paquetes/paquetes.component';
import { PaqueteEditorComponent } from './components/paquete-editor/paquete-editor.component';
import { RegisterUserComponent } from './components/register-user/register-user.component';
import { LoginComponent } from './components/login/login.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { HotelesService } from './services/hoteles.service';
import { AtraccionesService } from './services/atracciones.service';
import { PaquetesService } from './services/paquetes.service';
import { TransportesService } from './services/transportes.service';
import { UserService } from './services/user.service';
import { AuthService } from './services/auth.service';
import { PagosService } from './services/pagos.service';

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
        AtraccionEditorComponent,
        PaquetesComponent,
        PaqueteEditorComponent,
        RegisterUserComponent,
        LoginComponent,
        UserProfileComponent
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
            { path: 'atracciones', component: AtraccionesComponent },
            { path: 'register-user', component: RegisterUserComponent},
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [HotelesService, AtraccionesService, PaquetesService, TransportesService, UserService, AuthService, PagosService]
})
export class AppModuleShared {
}
