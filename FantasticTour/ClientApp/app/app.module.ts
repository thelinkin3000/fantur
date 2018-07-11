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
import { MailingService } from './services/mailing.service';
import { UserService } from './services/user.service';
import { AuthService } from './services/auth.service';
import { PagosService } from './services/pagos.service';
import { ContratarPaquetesService } from './services/contratar-paquetes.service';
import { TransportesComponent } from './components/transportes/transportes.component';
import { TransporteEditorComponent } from './components/transporte-editor/transporte-editor.component';
import { ErrorModalComponent } from './components/error-modal/error-modal.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { JwtHelperService } from '@auth0/angular-jwt';
import { EmailConfirmComponent } from './components/email-confirm/email-confirm.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule, MatInputModule } from '@angular/material';
import { TextInputAutocompleteModule } from 'angular-text-input-autocomplete';
import { EstadiasService } from './services/estadias.service';
import { EstadiasComponent } from './components/estadias/estadias.component';
import { EstadiaEditorComponent } from './components/estadia-editor/estadia-editor.component';
import { SendMailingComponent } from './components/send-mailing/send-mailing.component';
import { FroalaEditorModule, FroalaViewModule } from 'angular-froala-wysiwyg';
import { PagosComponent } from './components/pagos/pagos.component';
import { ContratarPaqueteComponent } from './components/contratar-paquete/contratar-paquete.component';
import {NgxMaskModule} from 'ngx-mask';
import { VerPaqueteComponent } from './components/ver-paquete/ver-paquete.component'





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
        UserProfileComponent,
        TransportesComponent,
        TransporteEditorComponent,
        ErrorModalComponent,
        EmailConfirmComponent,
        EstadiasComponent,
        EstadiaEditorComponent,
        SendMailingComponent,
        PagosComponent,
        ContratarPaqueteComponent,
        VerPaqueteComponent
    ],
    imports: [
        NgxMaskModule.forRoot(),
        CommonModule,
        FormsModule,
        HttpClientModule,
        ReactiveFormsModule,
        MatInputModule,
        MatAutocompleteModule,
        TextInputAutocompleteModule,
        NgbModule.forRoot(),
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
            { path: 'transportes', component: TransportesComponent},
            { path: 'transporte-editor', component: TransporteEditorComponent },
            { path: 'estadias', component: EstadiasComponent},
            { path: 'estadia-editor', component: EstadiaEditorComponent },
            { path: 'login', component: LoginComponent},
            { path: 'confirmation', component: EmailConfirmComponent},
            { path: 'paquete-editor', component: PaqueteEditorComponent},
            { path: 'send-mailing', component: SendMailingComponent},
            { path: 'pago', component: PagosComponent},
            { path: 'paquetes', component: PaquetesComponent},
            { path: 'ver-paquete', component: VerPaqueteComponent},
            { path: '**', redirectTo: 'home' }
        ])
    ],
    entryComponents: [ErrorModalComponent],
    providers: [MailingService, EstadiasService, HotelesService, AtraccionesService, PaquetesService, TransportesService, AuthService, PagosService, UserService, JwtHelperService, ContratarPaquetesService]
})
export class AppModuleShared {
}
