using MediatR;
using MovieShop.Business.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Domain.MediaR
{
    public class SetAddressRequest : Address, IRequest<SetAddressResponce>
    {
    }
}