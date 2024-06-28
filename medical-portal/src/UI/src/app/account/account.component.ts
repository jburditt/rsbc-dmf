import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { Role, RoleDescription } from '@app/features/auth/enums/identity-provider.enum';
import { AuthService } from '@app/features/auth/services/auth.service';
import { PidpService } from '@app/shared/api/services/pidp.service';
import { ProfileManagementService } from '@app/shared/services/profile.service';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [MatCardModule, MatIconModule, CommonModule, MatButtonModule],
  templateUrl: './account.component.html',
  styleUrl: './account.component.scss',
})
export class AccountComponent {
  isExpanded: Record<string, boolean> = {};
  fullName: string = "";
  email: string = "";
  role: string = "";

  public constructor(private pidpService: PidpService, private authService: AuthService, private profileManagementService: ProfileManagementService)
  {
    // this.profileManagementService.getCachedProfile().subscribe((profile) => {
    //   console.log("profile test", profile);
    // });
    // this.profileManagementService.getProfile().subscribe((profile) => {
    //   this.fullName = profile.firstName + " " + profile.lastName;
    //   if (profile.email)
    //     this.email = profile.email;
    //   if (profile.roles)
    //   {
    //     let roles = profile.roles
    //       .map((role) => Object.values(Role).find((value) => value === role))
    //       .filter((role) => role !== undefined && role !== Role.Enrolled) as Role[];
    //     this.role = roles
    //       .map((role) => RoleDescription.get(role))
    //       .join(", ");
    //   }
    // });

    // get roles directly from Keycloak
    // this.role = this.authService
    //   .getRoles()
    //   .map((role) => RoleDescription.get(role))
    //   .join(", ");

    // this.pidpService.apiPidpEndorsementsGet$Json().subscribe((data) =>
    // {
    //   console.log("endorsement response", data);
    // });
  }

  toggleIsExpandable(id?: string | null) {
    if (id) this.isExpanded[id] = !this.isExpanded[id];
  }
}
