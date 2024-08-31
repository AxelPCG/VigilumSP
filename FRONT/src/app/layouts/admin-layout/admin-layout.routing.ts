import { Routes } from '@angular/router';

import { HomeComponent } from '../../home-antiga/home.component';
import { UserComponent } from '../../user/user.component';
import { TablesComponent } from '../../tables/tables.component';
import { TypographyComponent } from '../../typography/typography.component';
import { IconsComponent } from '../../icons/icons.component';
import { MapsComponent } from '../../maps/maps.component';
import { NotificationsComponent } from '../../notifications/notifications.component';
import { UpgradeComponent } from '../../upgrade/upgrade.component';
import { HomeNovoComponent } from 'app/home/home.component';
import { SobreComponent } from 'app/sobre/sobre.component';
import { PrevisaoComponent } from 'app/previsao/previsao.component';
import { AlertaComponent } from 'app/alerta/alerta.component';
import { ReportarComponent } from 'app/reportar/reportar.component';

export const AdminLayoutRoutes: Routes = [
    { path: 'reportar',       component: ReportarComponent},
    { path: 'alertas',        component: AlertaComponent},
    { path: 'previsao',       component: PrevisaoComponent},
    { path: 'sobre',          component: SobreComponent},
    { path: 'dashboard',      component: HomeComponent },
    { path: 'home',           component: HomeNovoComponent},
    // { path: 'user',           component: UserComponent },
    // { path: 'table',          component: TablesComponent },
    // { path: 'typography',     component: TypographyComponent },
    // { path: 'icons',          component: IconsComponent },
    // { path: 'maps',           component: MapsComponent },
    // { path: 'notifications',  component: NotificationsComponent },
    // { path: 'upgrade',        component: UpgradeComponent },
];
