using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Ecode.PortalSystem.Mvc
{
	public class PortalableView<TModel> : PortalableView
	{
		// Fields
		private ViewDataDictionary<TModel> _viewData;

		// Methods
		public override void InitHelpers()
		{
			base.InitHelpers();
			this.Ajax = new AjaxHelper<TModel>(base.ViewContext, this);
			this.Html = new HtmlHelper<TModel>(base.ViewContext, this);
		}

		protected override void SetViewData(ViewDataDictionary viewData)
		{
			this._viewData = new ViewDataDictionary<TModel>(viewData);
			base.SetViewData(this._viewData);
		}

		// Properties
		public AjaxHelper<TModel> Ajax { get; set; }
		public HtmlHelper<TModel> Html { get; set; }

		public TModel Model
		{
			get
			{
				return this.ViewData.Model;
			}
		}

		public ViewDataDictionary<TModel> ViewData
		{
			get
			{
				if (this._viewData == null)
				{
					this.SetViewData(new ViewDataDictionary<TModel>());
				}
				return this._viewData;
			}
			set
			{
				this.SetViewData(value);
			}
		}

	}
}
