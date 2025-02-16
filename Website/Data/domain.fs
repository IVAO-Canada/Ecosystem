type VID = VID of string
type IcaoCode = ICAO of string

type FlightRules = IFR | VFR

type AtcRating =
    | AS1 | AS2 | AS3
    | ADC | APC | ACC
    | SEC | SAI | CAI

type PilotRating =
    | FS1 | FS2 | FS3
    | PP | SPP | CP
    | ATP | SFI | CFI

type User = {
    VID: VID
    AtcRating: AtcRating option
    PilotRating: PilotRating option
    Division: string
    StaffPositions: string list
    Supervisor: bool
    DiscordId: string
    Name: string
}

type EventLocation =
    | Division of string
    | Sector of string
    | Aerodrome of IcaoCode
    | Bridge of IcaoCode list

type Position =
    | ATIS of callsign: string
    | DEL of callsign: string
    | GND of callsign: string
    | TWR of callsign: string
    | APP of callsign: string
    | DEP of callsign: string
    | CTR of callsign: string
    | FSS of callsign: string

type ControllerSlot = {
    Position: Position
    User: User option
    Start: DateTimeOffset
    End: DateTimeOffset
}

type Pilot = {
    Callsign: string
    User: User
}

type PilotSlot = {
    FlightPlan: FlightPlan option
    Start: DateTimeOffset
    End: DateTimeOffset
}

type Flight = {
    Pilot: Pilot
    Departure: IcaoCode
    Arrival: IcaoCode
    TrackerSession: string option
}

type Event = {
    Name: string
    Location: EventLocation
    Controllers: ControllerSlot list
    Pilots: PilotSlot list
    Start: DateTimeOffset
    End: DateTimeOffset
}

type Role = {
    Name: string
    Colour: string option
}

type DiscordPermissions = {
    Read: Role list
    Write: Role list
    Admin: Role list
}

type VC = {
    ChannelId: string
    Name: string
    Permissions: DiscordPermissions
}

type TC = {
    ChannelId: string
    Name: string
    Permissions: DiscordPermissions
    SeedMessages: string list
}

type Channel =
    | VoiceChannel of VC
    | TextChannel of TC

type Category = Channel list * DiscordPermissions